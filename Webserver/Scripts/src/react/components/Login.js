import React, { Component } from 'react';

class Login extends Component {
    constructor(props) {
        super(props);
        this.submitForm = this.submitForm.bind(this);
        this.state = {
            email: '',
            password: '',
            validateEmailMsg: '',
            validatePasswordMsg: '',
            emailReadOnly: true,
            passwordReadOnly: true
        };
    }

    loginUser() {
        const { email, password } = this.state;

        //TODO: POST to LoginController
    }

    submitForm(e) {
        e.preventDefault();
        this.validateForm();

        const { emailMsg, passwordMsg } = this.state;

        if (emailMsg == '' || passwordMsg == '') {
            return;
        }

        this.loginUser();
    }

    handleUserInput(e) {
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ [name]: value });
    }

    validateForm() {
        let errorEmailText = !this.state.email ? <span className="text-danger">*Email field is required!</span> : ''
        let errorPasswordText = !this.state.password ? <span className="text-danger">*Password field is required!</span> : ''
        this.setState({ validateEmailMsg: errorEmailText, validatePasswordMsg: errorPasswordText })
    }

    render() {
        return (
            <form className="form-horizontal" onSubmit={this.submitForm}>
                <div className="form-group">
                    <label className="col-md-2 control-label">Email</label>
                    <div className="col-md-10">
                        <input type="text" name="email" className="form-control" defaultValue={this.state.email}
                            readOnly={this.state.emailReadOnly} onFocus={() => { this.setState({ emailReadOnly: false }) }} onInput={e => this.handleUserInput(e)} />
                        {this.state.validateEmailMsg}
                    </div>
                </div>
                <div className="form-group">
                    <label className="col-md-2 control-label">Password</label>
                    <div className="col-md-10">
                        <input type="password" name="password" className="form-control" defaultValue={this.state.password}
                            readOnly={this.state.passwordReadOnly} onFocus={() => { this.setState({ passwordReadOnly: false }) }} onInput={e => this.handleUserInput(e)} />
                        {this.state.validatePasswordMsg}
                    </div>
                </div>
                <div className="form-group">
                    <div className="col-md-offset-2 col-md-10">
                        <input type="submit" value="Log in" className="btn btn-default" />
                    </div>
                </div>
            </form>
        );
    }
}

export default Login;