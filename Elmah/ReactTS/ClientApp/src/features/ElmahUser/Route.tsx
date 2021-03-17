import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';
// import ListPage from 'src/features/ElmahUser/ListPage';
import DetailsPage from './DetailsPage';

export default function ElmahUserRoute(): JSX.Element {
	// <PrivateRoute path="/elmahuser" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/elmahuser/details/:user" exact={true} component={DetailsPage} />
    </Switch>);
}

