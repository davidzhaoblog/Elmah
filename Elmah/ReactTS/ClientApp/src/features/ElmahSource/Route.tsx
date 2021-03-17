import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';
// import ListPage from 'src/features/ElmahSource/ListPage';
import DetailsPage from './DetailsPage';

export default function ElmahSourceRoute(): JSX.Element {
	// <PrivateRoute path="/elmahsource" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/elmahsource/details/:source" exact={true} component={DetailsPage} />
    </Switch>);
}

