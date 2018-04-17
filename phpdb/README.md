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
* [Apache Userdir with SELinux on Fedora 27/25, CentOS/RHEL 7.4/6.9](https://www.if-not-true-then-false.com/2010/enable-apache-userdir-with-selinux-on-fedora-centos-red-hat-rhel/)
* [Install PEAR DB module under XAMPP](https://jerryoem.wordpress.com/2009/02/12/install-pear-db-module-under-xampp/)
* [php-pear broken on php 7.2](https://github.com/oerdnj/deb.sury.org/issues/668)