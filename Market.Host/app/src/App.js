import React, { Component } from 'react';

import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as testActions from './actions/testActions.js';
import PropTypes from 'prop-types';
import { Link } from 'react-router';
import darkBaseTheme from 'material-ui/styles/baseThemes/darkBaseTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import getMuiTheme from 'material-ui/styles/getMuiTheme';
import AppBar from 'material-ui/AppBar';
import Paper from 'material-ui/Paper';
import Menu from 'material-ui/Menu';
import MenuItem from 'material-ui/MenuItem';
import { grey900, cyan500, cyan700, grey400 } from 'material-ui/styles/colors';
import MenuComponent from './components/menu/MenuComponent';
import './App.css';

class App extends React.Component {
  constructor(props, context) {
    super(props, context);
  }

  render() {
    const customTheme = {
      palette: {
        primary1Color: cyan500,
        primary2Color: cyan700,
        primary3Color: grey400
      }
    };

    const style = {
      display: 'block',
      margin: '16px 32px 16px 0',
    };

    const theme = getMuiTheme(customTheme);

      return (
        <MuiThemeProvider muiTheme={getMuiTheme(darkBaseTheme)}>
        <div className="container-fluid">
          <div>
            <MenuComponent />
          </div>
          <div className="main-container">
            <div>{this.props.children}</div>
          </div>
        </div>
        <div className="footer">
          <p>Footer</p>
        </div>
      </MuiThemeProvider>
    );
  }
}

App.propTypes = {
  test: PropTypes.string,
  categories: PropTypes.array,
  children: PropTypes.object.isRequired
}

function mapStateToProps(state) {
  return {
    test: state.tests.test,
    categories: state.tests.categories
  }
}

function mapispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(testActions, dispatch)
  };
}

export default connect(mapStateToProps, mapispatchToProps)(App);
