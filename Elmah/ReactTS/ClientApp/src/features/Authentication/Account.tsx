import * as React from 'react';
import { Route, Switch } from 'react-router';
import LoginPage from './Login';
import { PrivateRoute } from './PrivateRoute';
import Profile from './Profile';

export default function Account(): JSX.Element {
    
    // const auth = (state: RootState) => state.auth;

    const renderLogin = () => {
        return (
            <LoginPage />
        );
    }

    return (<Switch>
        <PrivateRoute path="/account" exact={true} component={Profile} />
        <Route path={'/account/login'} render={renderLogin} />
    </Switch>);
}