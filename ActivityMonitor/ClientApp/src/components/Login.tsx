import * as React from 'react';
import { Button, Container, Form, FormGroup, Input, Label } from 'reactstrap';
import User from './User';

export default class Login extends React.PureComponent<{}, { children?: React.ReactNode }> {
    user?: User = undefined;

    public render() {
        console.log(this.user);
        return (
            <React.Fragment>
                <Form>
                    <FormGroup>
                        <Label>Username</Label>
                        <Input type='text'/>
                        <Button>Login</Button>
                    </FormGroup>
                </Form>
            </React.Fragment>
        );
    }    
}