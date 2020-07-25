import React from 'react';
import { Route, Redirect } from 'react-router-dom';


// Even though you could add a user object to the local storage and gain access to client side components,
// you won't be able to access the real data since it's secured with JWT 
export const PrivateRoute = ({ component: Component, ...rest }) => (
    <Route {...rest} render={props => (
        localStorage.getItem('user')
            ? <Component {...props} />
            : <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
    )} />
)