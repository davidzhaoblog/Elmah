import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';

import DetailsPage from './DetailsPage';

import IndexPage from './Index';



export default function ElmahHostRoute(): JSX.Element {
	// <PrivateRoute path="/elmahhost" exact={true} component={ListPage} />
    return (<Switch>

        <PrivateRoute path="/elmahhost" exact={true} component={IndexPage} />


        <PrivateRoute path="/elmahhost/details/:host" exact={true} component={DetailsPage} />
    </Switch>);
}

