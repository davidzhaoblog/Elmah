import React from 'react';
import { useSelector } from 'react-redux';
import { Route, Redirect, RouteProps } from 'react-router-dom';
import { RootState } from 'src/store/CombinedReducers';

export const PrivateRoute: React.FC<RouteProps> = ({ component: Component, ...rest }) => {
    const auth = useSelector((state: RootState) => state.auth);
    return (
        <Route {...rest} render={
            props => (
                auth && auth.isAuthenticated
                    ? <Component {...props} />
                    : <Redirect to={{ pathname: '/account/login', state: { from: props.location } }} />
            )} />
    )
}