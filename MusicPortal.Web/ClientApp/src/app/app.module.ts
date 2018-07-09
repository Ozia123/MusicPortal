import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ApplicationRef, Injectable } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTabsModule } from '@angular/material/tabs';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatListModule } from '@angular/material/list';
import { MatDialogModule } from '@angular/material/dialog';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSliderModule } from '@angular/material/slider';

import { ArtistService } from './services/artist/artist.service';
import { AlbumService } from './services/album/album.service';
import { TrackService } from './services/track/track.service';
import { PlayerService } from './services/player/player.service';

import { ArtistComponent } from './components/artist/artist.component';
import { TrackComponent } from './components/track/track.component';
import { ArtistsComponent } from './components/chart/artists/artists.component';
import { TracksComponent } from './components/chart/tracks/tracks.component'
import { ArtistProfileComponent } from './components/artist-profile/artist-profile.component';
import { AlbumComponent } from './components/album/album.component';
import { AlbumPageComponent } from './components/album-page/album-page.component';

import { environment } from '../environments/environment';
import { AngularFireModule } from 'angularfire2';

import { AngularFirestoreModule } from 'angularfire2/firestore';
import { AngularFireStorageModule } from 'angularfire2/storage';
import { UploadDialogComponent } from './components/track/upload-dialog/upload-dialog.component';
import { DropZoneDirective } from './directives/drop-zone.directive';
import { PlayerComponent } from './components/player/player.component';
import { FilteredArtistsComponent } from './components/filtered-artists/filtered-artists.component';
import { Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { FrameComponent } from './components/frame/frame.component';

import { SafePipe } from './pipes/safe-pipe.pipe';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ArtistComponent,
    TrackComponent,
    ArtistsComponent,
    TracksComponent,
    ArtistProfileComponent,
    AlbumComponent,
    AlbumPageComponent,
    UploadDialogComponent,
    DropZoneDirective,
    PlayerComponent,
    FilteredArtistsComponent,
    FrameComponent,
    SafePipe
  ],
  entryComponents: [
    UploadDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    HttpModule,
    FormsModule,
    MatIconModule,
    MatToolbarModule,
    MatMenuModule,
    MatListModule,
    MatTabsModule,
    MatSliderModule,
    MatButtonModule,
    MatGridListModule,
    FlexLayoutModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatSnackBarModule,
    MatPaginatorModule,
    MatDialogModule,
    BrowserModule,
    AngularFireModule.initializeApp(environment.firebase),
    AngularFirestoreModule,
    AngularFireStorageModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'chart/artists/:page', component: ArtistsComponent },
      { path: 'chart/tracks/:page', component: TracksComponent },
      { path: 'artist/:name', component: ArtistProfileComponent },
      { path: 'album/:name', component: AlbumPageComponent },
      { path: 'about', component: FrameComponent },
      { path: '**', redirectTo: '' }
    ])
  ],
  providers: [
    ArtistService,
    AlbumService,
    TrackService,
    PlayerService
  ],
  bootstrap: [AppComponent]
})

export class AppModule {
}
