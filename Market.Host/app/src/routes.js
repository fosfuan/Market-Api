import React from 'react';
import {Route, IndexRoute} from 'react-router';
import App from './App';
import HomePage from './components/home/HomePage';
import AboutPage from './components/about/AboutPage';
import LoginPage from './components/login/LoginPresentational';
import RegisterPage from './components/registration/RegistrationPresentational';

export default (
    <Route path="/" component={App}>
        <IndexRoute component={HomePage} />
        <Route path="about" component={AboutPage} />
        <Route path="login" component={LoginPage} />
        <Route path="register" component={RegisterPage} />
    </Route>
);