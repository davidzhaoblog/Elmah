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
import { WrapperTypes } from 'src/framework/ViewModels/IFormProps';

import { findPetsByStatus, petSelectors } from '../PetSlice';
import { orderBys, Pet, PetPaths, createPetDefault } from '../Pet';
import FindPetsByStatus from 'src/components/PetStore/Pet/FindPetsByStatus';
import FindPetsByStatusSearch from 'src/components/PetStore/Pet/FindPetsByStatusSearch';
import UpdatePet from 'src/components/PetStore/Pet/UpdatePet';
import AddPet from 'src/components/PetStore/Pet/AddPet';


export default function FindPetsByStatusPage(): JSX.Element {
  const classes = useStyles();
  const dispatch = useDispatch();
  const { t } = useTranslation(["UIStringResource", "UIStringResource_PetStore"]);

  const { findPetsByStatusParameters, orderBy, queryPagingSetting } = store.getState().pet;

  const [formType, setFormType] = useState('');
  const [selectedItem, setSelectedItem] = useState(null);

  const [openAdvancedSearchPopup, setOpenAdvancedSearchPopup] = useState(false);
  const [openAddPetPopup, setOpenAddPetPopup] = useState(false);
  const [openUpdatePetPopup, setOpenUpdatePetPopup] = useState(false);

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
    if(false)
	{}
    else if(type == PetPaths.AddPet)
      setOpenAddPetPopup(true);
    else if(type == PetPaths.UpdatePet)
      setOpenUpdatePetPopup(true);
    else
      return;

    setFormType(type);
    setSelectedItem(item);
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
		  <Button onClick={() => { openAdvancedSearchInPopup(PetPaths.FindPetsByStatus, null) }}>{t('UIStringResource_PetStore:FindPetsByStatus')}</Button>
          <Button onClick={() => { openFormInPopup(PetPaths.AddPet, null) }}>{t('UIStringResource_PetStore:AddPet')}</Button>
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
      {openAddPetPopup ? <AddPet type={formType} wrapperType={WrapperTypes.DialogForm}
        openPopup={openAddPetPopup}
        setOpenPopup={setOpenAddPetPopup}
        item={createPetDefault()}
      /> : null}



      {openUpdatePetPopup ? <UpdatePet type={formType} wrapperType={WrapperTypes.DialogForm}
        openPopup={openUpdatePetPopup}
        setOpenPopup={setOpenUpdatePetPopup}
        item={selectedItem}
      /> : null}


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

