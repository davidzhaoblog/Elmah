import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../../Authentication/PrivateRoute';
import StorePage from './StorePage';
import StorePage from './StorePage';
export default function StoreRoute(): JSX.Element {
	// <PrivateRoute path="/store" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/petstore/store" exact={true} component={StorePage} />
        <PrivateRoute path="/petstore/store/details/:errorId" exact={true} component={StorePage} />
    </Switch>);
}

