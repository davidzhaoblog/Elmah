import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';
import DetailsPage from './DetailsPage';
import IndexPage from './Index';
export default function UserRoute(): JSX.Element {
	// <PrivateRoute path="/user" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/user" exact={true} component={IndexPage} />
        <PrivateRoute path="/user/details/:errorId" exact={true} component={DetailsPage} />
    </Switch>);
}

