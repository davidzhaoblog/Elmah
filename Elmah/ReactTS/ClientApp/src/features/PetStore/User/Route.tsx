import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../../Authentication/PrivateRoute';
import UserPage from './UserPage';
import UserPage from './UserPage';
export default function UserRoute(): JSX.Element {
	// <PrivateRoute path="/user" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/petstore/user" exact={true} component={UserPage} />
        <PrivateRoute path="/petstore/user/details/:errorId" exact={true} component={UserPage} />
    </Switch>);
}

