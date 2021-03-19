import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';

import DetailsPage from './DetailsPage';

import IndexPage from './Index';



export default function ElmahUserRoute(): JSX.Element {
	// <PrivateRoute path="/elmahuser" exact={true} component={ListPage} />
    return (<Switch>

        <PrivateRoute path="/elmahuser" exact={true} component={IndexPage} />


        <PrivateRoute path="/elmahuser/details/:user" exact={true} component={DetailsPage} />
    </Switch>);
}

