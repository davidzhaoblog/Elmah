import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';
import DetailsPage from './DetailsPage';
import IndexPage from './Index';
export default function OrderRoute(): JSX.Element {
	// <PrivateRoute path="/order" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/order" exact={true} component={IndexPage} />
        <PrivateRoute path="/order/details/:errorId" exact={true} component={DetailsPage} />
    </Switch>);
}

