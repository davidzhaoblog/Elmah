import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';
// import ListPage from 'src/features/ELMAH_Error/ListPage';
import DetailsPage from './DetailsPage';

export default function ELMAH_ErrorRoute(): JSX.Element {
	// <PrivateRoute path="/elmah_error" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/elmah_error/details/:errorId" exact={true} component={DetailsPage} />
    </Switch>);
}
