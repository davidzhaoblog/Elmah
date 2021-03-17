import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';

import { showSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';
import { FormTypes, WrapperTypes } from 'src/framework/ViewModels/IFormProps';

import { elmahStatusCodeSelectors, getByIdentifier } from './Slice';
import { ElmahStatusCodeIdentifier } from './Types';
import Details from 'src/components/ElmahStatusCode/Details';

export default function DetailsPage(): JSX.Element {
  const { statusCode }: ElmahStatusCodeIdentifier = useParams()
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(showSpinner());
    dispatch(getByIdentifier({ statusCode }));

    // console.log('component mounted!')
  }, []) // notice the empty array here 

  const item = useSelector(
    (state: RootState) => elmahStatusCodeSelectors.selectById(state, statusCode)
  );

  return (
    <>
      {item &&
        <Details type={FormTypes.View} wrapperType={WrapperTypes.RegularPage}
          openPopup={false}
          setOpenPopup={() => { }}
          item={item}
        />
      }
    </>
  );
}

