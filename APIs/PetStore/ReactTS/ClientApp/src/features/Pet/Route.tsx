import * as React from 'react';
import { Switch } from 'react-router';
import { PrivateRoute } from '../../Authentication/PrivateRoute';

import GetPetByIdPage from './GetPetByIdPage';
import FindPetsByStatusPage from './FindPetsByStatusPage';
import FindPetsByTagsPage from './FindPetsByTagsPage';

export default function PetRoute(): JSX.Element {
	// <PrivateRoute path="/pet" exact={true} component={ListPage} />
    return (<Switch>
		<PrivateRoute path='/PetStore/Pet/GetPetById/:petId' exact={true} component={GetPetByIdPage} />
		<PrivateRoute path='/PetStore/Pet/FindPetsByStatus' exact={true} component={FindPetsByStatusPage} />
		<PrivateRoute path='/PetStore/Pet/FindPetsByTags' exact={true} component={FindPetsByTagsPage} />
    </Switch>);
}

