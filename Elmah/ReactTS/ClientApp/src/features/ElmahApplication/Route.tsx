import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';

import DetailsPage from './DetailsPage';

import IndexPage from './Index';



export default function ElmahApplicationRoute(): JSX.Element {
	// <PrivateRoute path="/elmahapplication" exact={true} component={ListPage} />
    return (<Switch>

        <PrivateRoute path="/elmahapplication" exact={true} component={IndexPage} />


        <PrivateRoute path="/elmahapplication/details/:application" exact={true} component={DetailsPage} />
    </Switch>);
}

