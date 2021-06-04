import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../../Authentication/PrivateRoute';

import GetOrderByIdPage from './GetOrderByIdPage';

export default function StoreRoute(): JSX.Element {
	// <PrivateRoute path="/store" exact={true} component={ListPage} />
    return (<Switch>
		<PrivateRoute path='/PetStore/Store/GetOrderById/:orderId' exact={true} component={GetOrderByIdPage} />
    </Switch>);
}

