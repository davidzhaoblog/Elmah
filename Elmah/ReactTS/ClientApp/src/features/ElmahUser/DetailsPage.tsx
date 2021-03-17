import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';

import { showSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';
import { FormTypes, WrapperTypes } from 'src/framework/ViewModels/IFormProps';

import { elmahUserSelectors, getByIdentifier } from './Slice';
import { ElmahUserIdentifier } from './Types';
import Details from 'src/components/ElmahUser/Details';

export default function DetailsPage(): JSX.Element {
  const { user }: ElmahUserIdentifier = useParams()
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(showSpinner());
    dispatch(getByIdentifier({ user }));

    // console.log('component mounted!')
  }, []) // notice the empty array here 

  const item = useSelector(
    (state: RootState) => elmahUserSelectors.selectById(state, user)
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

