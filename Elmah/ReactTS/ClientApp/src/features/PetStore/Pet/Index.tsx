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

import { findPetsByStatus, petSelectors } from './Slice';
// import { getIndexVM, elmahUserSelectors } from '../../ElmahUser/Slice';
import { orderBys, Pet } from '../Pet';
import Edit from 'src/components/PetStore/Pet/Edit';
import Search from 'src/components/PetStore/Pet//Search';
import List from 'src/components/PetStore/Pet//List';

export default function IndexPage(): JSX.Element {
  const classes = useStyles();
  const dispatch = useDispatch();
  const { t } = useTranslation(["UIStringResource", "UIStringResourcePerApp"]);

  const { findPetsByStatusCriteria, orderBy, queryPagingSetting } = store.getState().pet;

  const [openEditPopup, setOpenEditPopup] = useState(false);
  const [openAdvancedSearchPopup, setOpenAdvancedSearchPopup] = useState(false);
  const [formType, setFormType] = useState(FormTypes.Create);
  const [selectedItem, setSelectedItem] = useState(null);

  const handlePageChange = (event: object, value: number): void => {
    dispatch(showSpinner());
    dispatch(findPetsByStatus(findPetsByStatusCriteria));
  }

  const handlePageSizeChange = (event: React.ChangeEvent<{ name?: string; value: unknown }>) => {
    dispatch(showSpinner());
    dispatch(findPetsByStatus(findPetsByStatusCriteria));
  }

  const handleOrderByChange = (event: React.ChangeEvent<{ name?: string; value: unknown }>) => {
    dispatch(showSpinner());
    // var orderByHere = orderBys.find(o => o.expression === (event.target.value as string));
    dispatch(findPetsByStatus(findPetsByStatusCriteria));
  }
    
  const openAdvancedSearchInPopup = (type: FormTypes, item: Pet) => {
    setFormType(type);
    setOpenAdvancedSearchPopup(true);
    setSelectedItem(item);
  }

  const openFormInPopup = (type: FormTypes, item: Pet) => {
    setFormType(type);
    setOpenEditPopup(true);
    setSelectedItem(item);
  }

  const listItems = useSelector(
    (state: RootState) => petSelectors.selectAll(state)
  );

  useEffect(() => {
    dispatch(showSpinner());
    dispatch(findPetsByStatus(findPetsByStatusCriteria));

    // console.log('component mounted!')
  }, []) // notice the empty array here  

  return (
    <>
      <Paper className={classes.root}>
        <div className={classes.boxHeader}>
          <Typography className={classes.boxHeaderTitle}>{t('UIStringResourcePerApp:ElmahUser')}</Typography>
          <span className={classes.fillRemainingSpace} />
		  <Button onClick={() => { openAdvancedSearchInPopup(FormTypes.Create, null) }}>{t('UIStringResource:Search')}</Button>
          <Button onClick={() => { openFormInPopup(FormTypes.Create, null) }}>{t('UIStringResource:AddNew')}</Button>
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
          <List items={listItems} classes={classes} openFormInPopup={openFormInPopup} />
        </div>
      </Paper>
      {openEditPopup ? <Edit type={formType} wrapperType={WrapperTypes.DialogForm}
        openPopup={openEditPopup}
        setOpenPopup={setOpenEditPopup}
        item={selectedItem}
      /> : null}
      {openAdvancedSearchPopup ? <Search type={formType} wrapperType={WrapperTypes.DialogForm}
        openPopup={openAdvancedSearchPopup}
        setOpenPopup={setOpenAdvancedSearchPopup}
        criteria={findPetsByStatusCriteria}
        orderBy={orderBy}
        queryPagingSetting={queryPagingSetting}
      /> : null}
    </>
  );
}

