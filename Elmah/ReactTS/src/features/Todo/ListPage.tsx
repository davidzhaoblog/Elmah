import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { Button, Paper, Toolbar, Typography } from '@material-ui/core';
import { Pagination } from '@material-ui/lab';

import store from 'src/store/Store';
import { useStyles } from './styles';
import { showSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';
import { getIndexVM, todosSelectors } from './todoSlice';
import List from 'src/components/Todo/List';
import PageSizePicker from 'src/components/PageSizePicker';
import { pageSizeListCommon } from 'src/framework/GlobalVariables';
import OrderByPicker from 'src/components/OrderByPicker';
import { orderBys } from './types';

export default function TodoList(): JSX.Element {
  const classes = useStyles();
  const dispatch = useDispatch();

  const { orderBy, queryPagingSetting } = store.getState().todos;

  const handlePageChange = (event: object, value: number): void => {
    dispatch(showSpinner());
    dispatch(getIndexVM({ criteria: null, orderBy, queryPagingSetting: { ...queryPagingSetting, currentPage: value } }));
  }

  const handlePageSizeChange = (event: React.ChangeEvent<{ name?: string; value: unknown }>) => {
    dispatch(showSpinner());
    dispatch(getIndexVM({ criteria: null, orderBy, queryPagingSetting: { ...queryPagingSetting, currentPage: 1, pageSize: event.target.value as number } }));
  }
  
  const handleOrderByChange = (event: React.ChangeEvent<{ name?: string; value: unknown }>) => {
    dispatch(showSpinner());
    var orderByHere = orderBys.find(o => o.displayName === (event.target.value as string));
    dispatch(getIndexVM({ criteria: null, orderBy: orderByHere, queryPagingSetting: { ...queryPagingSetting, currentPage: 1 } }));
  }
  
  const listItems = useSelector(
    (state: RootState) => todosSelectors.selectAll(state)
  );


  useEffect(() => {
    dispatch(showSpinner());
    dispatch(getIndexVM({ criteria: null, orderBy, queryPagingSetting }));

    // console.log('component mounted!')
  }, []) // notice the empty array here  

  return (
    <Paper className={classes.root}>
      <div className={classes.boxHeader}>
        <Typography className={classes.boxHeaderTitle}>Todos</Typography>
        <span className={classes.fillRemainingSpace} />
        <Button>Delete all</Button>
      </div>
      <div>
        <Toolbar>
          <Pagination
            className="my-3"
            count={queryPagingSetting.countOfPages}
            page={queryPagingSetting.currentPage}
            siblingCount={1}
            boundaryCount={1}
            variant="outlined"
            shape="rounded"
            onChange={handlePageChange}
          />
          <div className={classes.rightToolbarItem}>
            <PageSizePicker
              classes={classes}
              pageSize={queryPagingSetting.pageSize}
              pageSizes={pageSizeListCommon}
              handlePageSizeChange={handlePageSizeChange}
            />
            <OrderByPicker
              classes={classes} 
              orderBy={orderBy.displayName} 
              orderBys={orderBys} 
              handleOrderByChange={handleOrderByChange} 
            />
          </div>
        </Toolbar>
        <List items={listItems} classes={classes} />
      </div>
    </Paper>
  );
}