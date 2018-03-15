import React, { Component } from 'react';
import '../../App.css';


class MenuItem extends React.Component {
    constructor(props, context) {
        super(props, context);
    }


    render() {
        return (
            <div className="menu-element">
                <div>{this.props.name}</div>
            </div>
        );
    }
}


export default MenuItem;