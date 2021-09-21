import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../user.service';
import { User } from '../interfaces/user';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
    errorMsg: string | undefined;
    form: FormGroup = new FormGroup({

      firstName: new FormControl(''),
      lastName: new FormControl(''),
      username: new FormControl(''),
      email: new FormControl(''),
      password: new FormControl(''),
      aboutMe: new FormControl(''),
      state: new FormControl(''),
      country: new FormControl(''),
      dob: new FormControl(''),
      phoneNumber: new FormControl('')
    });
    
    loading = false;
    submitted = false;
  
  
    users: User[] = [];
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private userService: UserService,
    private router: Router
  ) { }

  ngOnInit(){
      
      this.form = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      username: ['', [Validators.required, Validators.minLength(5)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      aboutMe: ['', Validators.required],
      state: ['', Validators.required],
      country: ['', Validators.required],
      dob: ['', Validators.required],
      phoneNumber: ['', Validators.required]
  });
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {
      this.submitted = true;
       //stop here if form is invalid
      if (this.form.invalid) {
          return;
      }
      
      this.loading = true;
      //this.id = this.route.snapshot.params['id'];
      this.userService.addUser(this.form.value)
        .pipe(first())
        .subscribe(
          data => {
            this.router.navigate(['../login'], {relativeTo: this.route});
            alert("Register successfully!");
          },
          error => { 
            this.loading = false;
            alert(error);
          }
        )

  }


}
