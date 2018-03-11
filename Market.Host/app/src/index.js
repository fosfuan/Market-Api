import React from 'react';
import ReactDOM from 'react-dom';
import configureStore from './stores/configureStore';
import {Provider} from 'react-redux';
import { Router, browserHistory } from 'react-router';
import './index.css';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import {loadTests} from './actions/testActions';
import {loadCategory} from './actions/testActions';

const store = configureStore();
store.dispatch(loadTests());

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById('root')
)

registerServiceWorker();
