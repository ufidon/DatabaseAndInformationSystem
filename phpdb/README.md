# How to enable userdir
enable: domain_name/~username/

## Apache
* enable userdir module
```bash
sudo a2enmod userdir
```

* modify /etc/apache2/mods-enabled/userdir.conf
```html
<IfModule mod_userdir.c>
     UserDir public_html
     UserDir disabled root

        <Directory /home/*/public_html>
             AllowOverride FileInfo AuthConfig Limit Indexes
             Options MultiViews Indexes SymLinksIfOwnerMatch IncludesNoExec
             Require method GET POST OPTIONS
        </Directory>
</IfModule>

# vim: syntax=apache ts=4 sw=4 sts=4 sr noet
```

* create user directory & restart apache2
```bash
mkdir ~/public_html
sudo systemctl restart apache2
```




## NGINX

## References
* [HOW TO ENABLE USERDIR FOR APACHE2 / NGINX ON UBUNTU 17.04 / 17.10](https://websiteforstudents.com/enable-userdir-apache2-nginx-ubuntu-17-04-17-10/)

