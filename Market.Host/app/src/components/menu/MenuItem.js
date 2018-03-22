import React, { Component } from 'react';
import {Link} from 'react-router';
import { browserHistory } from 'react-router';
import '../../App.css';


class MenuItem extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.redirectToHomeIfPageNotInItems = this.redirectToHomeIfPageNotInItems.bind(this);
    }

    redirectToHomeIfPageNotInItems(){
        let itemName = this.props.name;
        const items =  ["About", "Login", "Register"];
        if(items.indexOf(itemName) > -1){            
            browserHistory.push(this.props.name.toLowerCase());
        }
        else{
            browserHistory.push('/');
        }
    }

    render() {
        return (
            <div className="menu-element" onClick={this.redirectToHomeIfPageNotInItems}>
                <div>{this.props.name}</div>
            </div>
        );
    }
}


export default MenuItem;