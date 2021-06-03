import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../../Authentication/PrivateRoute';
import PetPage from './PetPage';
import PetPage from './PetPage';
export default function PetRoute(): JSX.Element {
	// <PrivateRoute path="/pet" exact={true} component={ListPage} />
    return (<Switch>
        <PrivateRoute path="/petstore/pet" exact={true} component={PetPage} />
        <PrivateRoute path="/petstore/pet/details/:errorId" exact={true} component={PetPage} />
    </Switch>);
}

