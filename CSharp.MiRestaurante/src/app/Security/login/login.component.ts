import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ClrFormsModule, ClrDropdownModule } from '@clr/angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone: true,
  imports: [FormsModule, ClrFormsModule, ClrDropdownModule],
})
export class LoginComponent {
  form = {
    type: 'local',
    username: '',
    password: '',
    rememberMe: false,
  };
}
