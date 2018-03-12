import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';

import {bindActionCreators} from 'redux';
import {connect} from 'react-redux';
import * as testActions from './actions/testActions.js';
import PropTypes from 'prop-types';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';

class App extends React.Component {
constructor(props, context) {
        super(props, context);
    }

  render() {
    return (
      <MuiThemeProvider>
          <div className="App">
            {this.props.test}
            {this.props.categories.map((category, index) => {
                return (
                  <div key={index}>
                    <p>{category}</p>
                  </div>
                );
              }            
            )}
          </div>
      </MuiThemeProvider>
    );
  }
}

App.propTypes = {
    test: PropTypes.string,
    categories: PropTypes.array
}

function mapStateToProps(state) {
  return {
    test: state.tests.test,
    categories: state.tests.categories
  }
}

function mapispatchToProps(dispatch){
    return{
        actions: bindActionCreators(testActions, dispatch)
    };
}

export default connect(mapStateToProps, mapispatchToProps)(App);
