import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';
// import ListPage from 'src/features/ElmahStatusCode/ListPage';
import DetailsPage from './DetailsPage';

export default function ElmahStatusCodeRoute(): JSX.Element {
	// <PrivateRoute path="/elmahstatuscode" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/elmahstatuscode/details/:statusCode" exact={true} component={DetailsPage} />
    </Switch>);
}

