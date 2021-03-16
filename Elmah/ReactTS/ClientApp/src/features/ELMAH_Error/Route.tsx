import * as React from 'react';
import { Switch } from 'react-router';
import ListPage from 'src/features/ELMAH_Error/ListPage';
import { PrivateRoute } from '../Authentication/PrivateRoute';
import DetailsPage from './DetailsPage';

export default function ELMAH_ErrorRoute(): JSX.Element {
    return (<Switch>
        <PrivateRoute path="/elmah_error" exact={true} component={ListPage} />
        <PrivateRoute path="/elmah_error/details/:errorId" exact={true} component={DetailsPage} />
    </Switch>);
}