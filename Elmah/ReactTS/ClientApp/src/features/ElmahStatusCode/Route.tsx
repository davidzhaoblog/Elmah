import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';

import DetailsPage from './DetailsPage';

import ListPage from 'src/features/ElmahStatusCode/IndexPage';



export default function ElmahStatusCodeRoute(): JSX.Element {
	// <PrivateRoute path="/elmahstatuscode" exact={true} component={ListPage} />
    return (<Switch>

        <PrivateRoute path="/elmahstatuscode" exact={true} component={IndexPage} />


        <PrivateRoute path="/elmahstatuscode/details/:statusCode" exact={true} component={DetailsPage} />
    </Switch>);
}

