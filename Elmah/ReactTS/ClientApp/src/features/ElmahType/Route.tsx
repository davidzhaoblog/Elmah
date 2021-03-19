import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../Authentication/PrivateRoute';

import DetailsPage from './DetailsPage';

import ListPage from 'src/features/ElmahType/IndexPage';



export default function ElmahTypeRoute(): JSX.Element {
	// <PrivateRoute path="/elmahtype" exact={true} component={ListPage} />
    return (<Switch>

        <PrivateRoute path="/elmahtype" exact={true} component={IndexPage} />


        <PrivateRoute path="/elmahtype/details/:type" exact={true} component={DetailsPage} />
    </Switch>);
}

