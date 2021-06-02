import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../../Authentication/PrivateRoute';
import DetailsPage from './DetailsPage';
import IndexPage from './IndexPage';
export default function StoreRoute(): JSX.Element {
	// <PrivateRoute path="/store" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/petstore/store" exact={true} component={IndexPage} />
        <PrivateRoute path="/petstore/store/details/:errorId" exact={true} component={DetailsPage} />
    </Switch>);
}

