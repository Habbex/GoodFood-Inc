import React from 'react';
import { render } from 'react-dom';
import { Provider } from 'react-redux';
import {Container} from "@material-ui/core";
import {ToastProvider}  from "react-toast-notifications"
import { store } from './_helpers';
import { App } from './App';


render(
    <Provider store={store}>
           <ToastProvider autoDismiss={true}>
           <Container maxWidth="lg"></Container>
           <App />
           </ToastProvider>    
    </Provider>,
    document.getElementById('app')
);