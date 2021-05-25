import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';
import DetailsPage from './DetailsPage';
import IndexPage from './Index';
export default function PetRoute(): JSX.Element {
	// <PrivateRoute path="/pet" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/pet" exact={true} component={IndexPage} />
        <PrivateRoute path="/pet/details/:errorId" exact={true} component={DetailsPage} />
    </Switch>);
}

