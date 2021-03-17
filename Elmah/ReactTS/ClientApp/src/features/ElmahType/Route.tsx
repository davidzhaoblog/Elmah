import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';
// import ListPage from 'src/features/ElmahType/ListPage';
import DetailsPage from './DetailsPage';

export default function ElmahTypeRoute(): JSX.Element {
	// <PrivateRoute path="/elmahtype" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/elmahtype/details/:type" exact={true} component={DetailsPage} />
    </Switch>);
}

