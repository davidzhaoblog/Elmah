import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';

import { showSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';
import { FormTypes, WrapperTypes } from 'src/framework/ViewModels/IFormProps';

import { eLMAH_ErrorSelectors, getByIdentifier } from './Slice';
import { ELMAH_ErrorIdentifier } from './Types';
import Details from 'src/components/ELMAH_Error/Details';

export default function DetailsPage(): JSX.Element {
  const { errorId }: ELMAH_ErrorIdentifier = useParams()
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

