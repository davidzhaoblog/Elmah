import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';

import { showSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';
import { FormTypes, WrapperTypes } from 'src/framework/ViewModels/IFormProps';

import { elmahHostSelectors, getByIdentifier } from './Slice';
import { ElmahHostIdentifier } from './Types';
import Details from 'src/components/ElmahHost/Details';

export default function DetailsPage(): JSX.Element {
  const { host }: ElmahHostIdentifier = useParams()
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(showSpinner());
    dispatch(getByIdentifier({ host }));

    // console.log('component mounted!')
  }, []) // notice the empty array here 

  const item = useSelector(
    (state: RootState) => elmahHostSelectors.selectById(state, host)
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

