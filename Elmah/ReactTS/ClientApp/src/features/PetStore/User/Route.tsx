import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../../Authentication/PrivateRoute';

import GetUserByNamePage from './GetUserByNamePage';

export default function UserRoute(): JSX.Element {
	// <PrivateRoute path="/user" exact={true} component={ListPage} />
    return (<Switch>
		<PrivateRoute path='/PetStore/User/GetUserByName/:username' exact={true} component={GetUserByNamePage} />
    </Switch>);
}

