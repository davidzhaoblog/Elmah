import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';

import { showSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';
import { FormTypes, WrapperTypes } from 'src/framework/ViewModels/IFormProps';

import { elmahTypeSelectors, getByIdentifier } from './Slice';
import { ElmahTypeIdentifier } from './Types';
import Details from 'src/components/ElmahType/Details';

export default function DetailsPage(): JSX.Element {
  const { type }: ElmahTypeIdentifier = useParams()
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(showSpinner());
    dispatch(getByIdentifier({ type }));

    // console.log('component mounted!')
  }, []) // notice the empty array here 

  const item = useSelector(
    (state: RootState) => elmahTypeSelectors.selectById(state, type)
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

