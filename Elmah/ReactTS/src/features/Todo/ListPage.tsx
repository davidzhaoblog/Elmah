import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from 'src/store/CombinedReducers';
import { getIndexVM, todosSelectors } from './todoSlice';
import { showSpinner } from 'src/layout/appSlice';
import { useStyles } from './styles';
// import { createListState } from 'src/framework/IListState';
import { Button, Paper, Typography } from '@material-ui/core';
import List from 'src/components/Todo/List';
import store from 'src/store/Store';

export default function TodoList(): JSX.Element {
  const classes = useStyles();
  const dispatch = useDispatch();

  const [pageSizes, setPageSizes] = useState([]);
  const queryPagingSetting = store.getState().todos.queryPagingSetting;

  const handlePageChange = (event: object, value: number): void => {
    dispatch(showSpinner());
    dispatch(getIndexVM({ criteria: null, queryPagingSetting: { ...queryPagingSetting, currentPage: value } }));
  }

  const listItems = useSelector(
    (state: RootState) => todosSelectors.selectAll(state)
  );


  useEffect(() => {
    dispatch(showSpinner());
    setPageSizes([10,25,100]);
    dispatch(getIndexVM({ criteria: null, queryPagingSetting }));

    // console.log('component mounted!')
  }, []) // notice the empty array here  

  return (
    <Paper className={classes.root}>
      <div className={classes.boxHeader}>
        <Typography className={classes.boxHeaderTitle}>Todos</Typography>
        <span className={classes.fillRemainingSpace} />
        <Button>Delete all</Button>
      </div>
      <List items={listItems} classes={classes} handlePageChange={handlePageChange} page={queryPagingSetting.currentPage} count={queryPagingSetting.countOfPages} pageSize={queryPagingSetting.pageSize} pageSizes={pageSizes} />
    </Paper>
  );
}