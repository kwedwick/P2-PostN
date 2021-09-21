
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { AuthService } from '../auth.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formGroup = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  });
  invalidLogin = new Boolean;

  constructor(private authService: AuthService, 
    private fb: FormBuilder, 
    private router: Router, 
    private location: Location,
    private route: ActivatedRoute,) { }

    returnUrl: any;

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  loginProcess() {
    if (this.formGroup.valid) {
      const loginObserver = {
        next: (x: any) => {
          alert('Welcome back ' + x.username);
          this.router.navigateByUrl(this.returnUrl);
        },
        error: (err: any) => {
          console.log(err);
          alert('Unable to Login. Please check your credentials.');
        },
      };
      this.authService.login(this.formGroup.value)
        .subscribe(loginObserver)
    } else {
      alert("Please enter in your information!");
    }
  }

}
