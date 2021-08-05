import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  @Input() user: User;

  @Output() updateData = new EventEmitter();

  isEditerMode: boolean = false;

  constructor(
    private userService: UserService,
    private router: Router) { }

  ngOnInit(): void {
    console.dir('onInitLogin')
    /* 
    this.userService.getUser().subscribe(result => {
      if (!result) {
        this.router.navigateByUrl('/login');
      }else {
        this.user = result
      }
    },
      error => {
      }) */

    console.dir('onInitLoginEnd')
  }


  startEditing() {
    this.isEditerMode = true;
    console.dir(this.isEditerMode)
  }

  saveChanges() {
    this.updateData.emit();

    this.isEditerMode = true;
  }
}
