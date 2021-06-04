import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';

import { showSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';
import { FormTypes, WrapperTypes } from 'src/framework/ViewModels/IFormProps';

import { orderSelectors, getOrderById } from '../OrderSlice';
import Order from 'src/components/PetStore/Order/Order;


export default function GetOrderByIdPage(): JSX.Element {
  const { orderId }: {orderId: integer} = useParams()
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(showSpinner());
    dispatch(getOrderById({ id: orderId }));

    // console.log('component mounted!')
  }, []) // notice the empty array here 

  const item = useSelector(
    (state: RootState) => orderSelectors.selectById(state, orderId)
  );

  return (
    <>
      {item &&
        <Order type={FormTypes.View} wrapperType={WrapperTypes.RegularPage}
          openPopup={false}
          setOpenPopup={() => { }}
          item={item}
        />
      }
    </>
  );
}

