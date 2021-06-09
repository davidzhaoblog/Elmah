import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { Button, Paper, Toolbar, Typography } from '@material-ui/core';
import { Pagination } from '@material-ui/lab';
import { useTranslation } from 'react-i18next';

import store from 'src/store/Store';
import { useStyles } from '../../listStyles';
import { showSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';
import PageSizePicker from 'src/components/PageSizePicker';
import { pageSizeListCommon } from 'src/framework/GlobalVariables';
import OrderByPicker from 'src/components/OrderByPicker';
import { FormTypes, WrapperTypes } from 'src/framework/ViewModels/IFormProps';

import { findPetsByStatus, petSelectors } from '../PetSlice';
import { orderBys, Pet } from '../Pet';
import FindPetsByStatus from 'src/components/PetStore/Pet/FindPetsByStatus';
import FindPetsByStatusSearch from 'src/components/PetStore/Pet/FindPetsByStatusSearch';


export default function FindPetsByStatusPage(): JSX.Element {
  const classes = useStyles();
  const dispatch = useDispatch();
  const { t } = useTranslation(["UIStringResource", "UIStringResource_PetStore"]);

  const { findPetsByStatusParameters, orderBy, queryPagingSetting } = store.getState().pet;

  const [openAdvancedSearchPopup, setOpenAdvancedSearchPopup] = useState(false);
  const [formType, setFormType] = useState('Create');

  const handlePageChange = (event: object, value: number): void => {
    dispatch(showSpinner());
    dispatch(findPetsByStatus(findPetsByStatusParameters));
  }

  const handlePageSizeChange = (event: React.ChangeEvent<{ name?: string; value: unknown }>) => {
    dispatch(showSpinner());
    dispatch(findPetsByStatus(findPetsByStatusParameters));
  }

  const handleOrderByChange = (event: React.ChangeEvent<{ name?: string; value: unknown }>) => {
    dispatch(showSpinner());
    // var orderByHere = orderBys.find(o => o.expression === (event.target.value as string));
    dispatch(findPetsByStatus(findPetsByStatusParameters));
  }
    
  const openAdvancedSearchInPopup = (type: string, item: Pet) => {
    setFormType(type);
    setOpenAdvancedSearchPopup(true);
    // setSelectedItem(item);
  }

  const openFormInPopup = (type: string, item: Pet) => {
    setFormType(type);
    // setOpenEditPopup(true);
    // setSelectedItem(item);
  }

  const listItems = useSelector(
    (state: RootState) => petSelectors.selectAll(state)
  );

  useEffect(() => {
    dispatch(showSpinner());
    dispatch(findPetsByStatus(findPetsByStatusParameters));

    // console.log('component mounted!')
  }, []) // notice the empty array here  

  return (
    <>
      <Paper className={classes.root}>
        <div className={classes.boxHeader}>
          <Typography className={classes.boxHeaderTitle}>{t('UIStringResource_PetStore:Pet')}</Typography>
          <span className={classes.fillRemainingSpace} />
		  <Button onClick={() => { openAdvancedSearchInPopup(FormTypes.Create, null) }}>{t('UIStringResource:Search')}</Button>
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
                orderBy={orderBy?.expression}
                orderBys={orderBys}
                handleOrderByChange={handleOrderByChange}
              />
            </div>
          </Toolbar>
          <FindPetsByStatus items={listItems} classes={classes} openFormInPopup={openFormInPopup} />
        </div>
      </Paper>
      {openAdvancedSearchPopup ? <FindPetsByStatusSearch type={formType} wrapperType={WrapperTypes.DialogForm}
        openPopup={openAdvancedSearchPopup}
        setOpenPopup={setOpenAdvancedSearchPopup}
        criteria={findPetsByStatusParameters}
        orderBy={orderBy}
        queryPagingSetting={queryPagingSetting}
      /> : null}
    </>
  );
}

