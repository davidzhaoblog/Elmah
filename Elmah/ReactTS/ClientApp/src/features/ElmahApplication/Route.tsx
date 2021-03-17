import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';
// import ListPage from 'src/features/ElmahApplication/ListPage';
import DetailsPage from './DetailsPage';

export default function ElmahApplicationRoute(): JSX.Element {
	// <PrivateRoute path="/elmahapplication" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/elmahapplication/details/:application" exact={true} component={DetailsPage} />
    </Switch>);
}

