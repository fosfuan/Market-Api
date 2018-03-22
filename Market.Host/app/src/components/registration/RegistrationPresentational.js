import React, { Component } from 'react';
import TextField from 'material-ui/TextField';
import '../../App.css';
import { Link } from 'react-router';
import List from 'material-ui/List/List';
import ListItem from 'material-ui/List/ListItem';
import Avatar from 'material-ui/Avatar';
import {
    blue300,
    indigo900,
    orange200,
    deepOrange300,
    pink400,
    purple500,
} from 'material-ui/styles/colors';
import {
    Step,
    Stepper,
    StepLabel,
    StepContent,
} from 'material-ui/Stepper';
import RaisedButton from 'material-ui/RaisedButton';
import FlatButton from 'material-ui/FlatButton';

class RegistrationPresentatonal extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.emailOnChange = this.emailOnChange.bind(this);
        this.handleNext = this.handleNext.bind(this);
        this.passwordOnChange = this.passwordOnChange.bind(this);
    }

    state = {
        finished: false,
        stepIndex: 0,
        username: '',
        username_error: '',
        password: '',
        password_error: '',
        email: '',
        email_error: '',
        repeat_password: '',
        repeat_password_error: '',
        fontEmailColor : {
            color: 'rgba(255, 255, 255, 0.3)'
        },
        fontPasswordColor : {
            color: 'rgba(255, 255, 255, 0.3)'
        },
        fontRepeatPasswordColor : {
            color: 'rgba(255, 255, 255, 0.3)'
        },
        fontUsernameColor : {
            color: 'rgba(255, 255, 255, 0.3)'
        }
    };

    emailOnChange = (event) => {
        const emailError = 'Provide correct email address!';
        event.preventDefault();
        let emailValue = event.target.value;
        if(emailValue.length < 1){
            this.setState({ email_error: emailError });  
            this.setState({ email: emailValue });
        }
        if (emailValue.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i)) {
            this.setState({ email: emailValue });
            this.setState({ email_error: '', fontEmailColor: {color: 'white'}});
            this.handleNext(1);
        } else {
            this.setState({ email: emailValue });
            this.setState({ email_error: emailError });
            this.handlePrev();
        }
    }

    passwordOnChange = (event) => {
        event.preventDefault();
        let passwordValue = event.target.value;
        this.setState({ password: passwordValue });
        if (passwordValue.length < 1) {
            const passwordError = 'Provide Password!';
            this.setState({ password: passwordValue });
            this.setState({ password_error: passwordError });
            this.handlePrev();
        }
        else if (passwordValue.length > 0) {
            this.setState({ password_error: '' });
            this.setState({ password: passwordValue, fontPasswordColor: {color: 'white'}});
            this.handleNext(1);
        }
    }

    usernameOnChange = (event) =>{
        event.preventDefault();
        this.setState({ username: usernameValue });        
        let usernameValue = event.target.value;
        if(usernameValue.length < 7){
            this.setState({ username_error: 'Username must be longer than 7 signs!' });
        }else if(!/[A-Z]/.test(usernameValue)){
            this.setState({ username_error: 'Username must have at least one upper case letter!' });
        }else{
            this.setState({ username_error: '' });
            this.handleNext(0);
        }

    }

    handleNext = () => {
        const { stepIndex } = this.state;
        this.setState({
            stepIndex: stepIndex + 1,
            finished: stepIndex >= 4,
        });
    };

    handlePrev = () => {
        const { stepIndex } = this.state;
        if (stepIndex > 0) {
            this.setState({ stepIndex: stepIndex - 1 });
        }
    };
    render() {
        const style = {
            textAlign: 'center',
            color: 'white'
        };

        let fontColor = {
            color: 'rgba(255, 255, 255, 0.3)'
        }
        const { finished, stepIndex } = this.state;
        return (
            <div className="login-container">
                <div className="login-element" >
                    <h2 style={style}>Registration</h2>
                    <TextField
                        floatingLabelText="Username"
                        onChange={this.usernameOnChange}
                        value={this.state.username}
                        errorText={this.state.username_error}
                    /><br />
                    <TextField
                        floatingLabelText="Email"
                        onChange={this.emailOnChange}
                        value={this.state.email}
                        errorText={this.state.email_error}
                    /><br />
                    <TextField
                        floatingLabelText="Password"
                        type="password"
                        onChange={this.passwordOnChange}
                        value={this.state.password}
                        errorText={this.state.password_error}
                    /><br />
                    <TextField
                        floatingLabelText="Repeat Password"
                        type="password"
                        onChange={this.repeatPasswordOnChange}
                        value={this.state.repeat_password}
                        errorText={this.state.repeat_password_error}
                    /><br />
                </div>
                <div className="login-element login-stepper">
                    <div style={{ maxWidth: 180, maxHeight: 400, margin: 'auto' }}>
                        <Stepper activeStep={stepIndex} orientation="vertical">
                            <Step>
                                <StepLabel style={this.state.fontEmailColor}>Username</StepLabel>
                                <StepContent>
                                    <p>
                                        Provide an Username
                                    </p>
                                </StepContent>
                            </Step>
                            <Step>
                                <StepLabel style={this.state.fontEmailColor}>Email</StepLabel>
                                <StepContent>
                                    <p>
                                        Provide an email
                                    </p>
                                </StepContent>
                            </Step>
                            <Step>
                                <StepLabel style={this.state.fontPasswordColor}>Password</StepLabel>
                                <StepContent>
                                    <p>Provide a password.</p>
                                </StepContent>
                            </Step>
                            <Step>
                                <StepLabel style={this.state.fontPasswordColor}>Repeat Password</StepLabel>
                                <StepContent>
                                    <p>You have to repeat password which you provided.</p>
                                </StepContent>
                            </Step>
                        </Stepper>
                    </div>
                </div>
                    {finished && (
                        <div className="menu-element centered-button" >
                            <div>Login</div>
                        </div>)}
            </div>
        );
    }
}


export default RegistrationPresentatonal;