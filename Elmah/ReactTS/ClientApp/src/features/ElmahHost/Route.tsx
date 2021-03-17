import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';
// import ListPage from 'src/features/ElmahHost/ListPage';
import DetailsPage from './DetailsPage';

export default function ElmahHostRoute(): JSX.Element {
	// <PrivateRoute path="/elmahhost" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/elmahhost/details/:host" exact={true} component={DetailsPage} />
    </Switch>);
}

