import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import Details from 'src/components/ELMAH_Error/Details';
import { FormTypes, WrapperTypes } from 'src/framework/ViewModels/IFormProps';

import { showSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';
import { eLMAH_ErrorSelectors, getByIdentifier } from './elmah_ErrorSlice';
import { ELMAH_ErrorIdentifier } from './types';

export default function DetailsPage(): JSX.Element {
  const { errorId } : ELMAH_ErrorIdentifier = useParams()
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(showSpinner());
    dispatch(getByIdentifier({ errorId }));

    // console.log('component mounted!')
  }, []) // notice the empty array here 

  const item = useSelector(
    (state: RootState) => eLMAH_ErrorSelectors.selectById(state, errorId)
  );

  return (
    <Details type={FormTypes.Create} wrapperType={WrapperTypes.RegularPage}
      openPopup={false}
      setOpenPopup={()=>{}}
      item={item}
  />
  );
}