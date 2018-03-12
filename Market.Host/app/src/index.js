import React from 'react';
import {render} from 'react-dom';
import ReactDOM from 'react-dom';
import configureStore from './stores/configureStore';
import {Provider} from 'react-redux';
import { Router, Route, browserHistory } from 'react-router';
import routes from './routes';
import App from './App';
import About from './components/about/AboutPage';
import registerServiceWorker from './registerServiceWorker';
import {loadTests} from './actions/testActions';
import {loadCategory} from './actions/testActions';

const store = configureStore();
store.dispatch(loadTests());

ReactDOM.render(
<Provider store={store}>
    <Router history={browserHistory} routes={routes} />
</Provider>,
  // <Provider store={store}>
  //   <Router history={browserHistory}>
  //     <Route path="/" component="App">
  //         <Route path="/about" component={About} />
  //     </Route>
  //   </Router>
  // </Provider>,
  document.getElementById('root'));

registerServiceWorker();
