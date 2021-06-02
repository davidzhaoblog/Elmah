import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../../Authentication/PrivateRoute';
import DetailsPage from './DetailsPage';
import IndexPage from './IndexPage';
export default function UserRoute(): JSX.Element {
	// <PrivateRoute path="/user" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/petstore/user" exact={true} component={IndexPage} />
        <PrivateRoute path="/petstore/user/details/:errorId" exact={true} component={DetailsPage} />
    </Switch>);
}

