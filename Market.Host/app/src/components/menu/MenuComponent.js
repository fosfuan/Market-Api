import React, { Component } from 'react';
import MenuItem from './MenuItem';
import '../../App.css';

class MenuComponent extends React.Component {
    constructor(props, context) {
        super(props, context);
    }

    render() {

        const menuItem = ["About", "Home", "Login"];

        return (
            <div  className="menu-wrapper">
                <div className="menu-container">
                    {
                        menuItem.map((item, index) => {
                            return <MenuItem name={item}/>;
                        }
                        )}
                </div>
            </div>
        );
    }
}

export default MenuComponent;