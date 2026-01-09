var app = angular.module('UngDungGiaoVien', []);

app.controller('DiemDanhController', function($scope, $http) {
    
    // CẤU HÌNH ĐƯỜNG DẪN (Chỉnh port cho đúng với Swagger)
    var currentUrl = "http://localhost:6002"; // Theo ảnh Swagger của bạn là 6002
    var apiBase = currentUrl + "/api/DiemDanh";

    $scope.dsDiemDanh = [];
    
    // KHỞI TẠO NGÀY HIỆN TẠI (Fix lỗi hiện 1990)
    $scope.ngayChon = new Date(); 
    
    // ĐỂ RỖNG ĐỂ LOAD TẤT CẢ (Hoặc để "MAM01" nếu muốn lọc)
    $scope.maLop = ""; 

    // Hàm format ngày (yyyy-MM-dd)
    function formatDate(date) {
        if (!date) return "";
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();
        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;
        return [year, month, day].join('-');
    }

    // Hàm parse giờ
    function parseTime(timeStr) {
        if (!timeStr) return null;
        var d = new Date();
        var parts = timeStr.split(':');
        if (parts.length >= 2) {
            d.setHours(parseInt(parts[0]));
            d.setMinutes(parseInt(parts[1]));
            return d;
        }
        return null;
    }

    // --- LOAD DỮ LIỆU ---
    $scope.taiDuLieu = function() {
        var strNgay = formatDate($scope.ngayChon);
        
        // Gọi API
        var url = apiBase + "/get-all?ngay=" + strNgay;
        
        // Chỉ thêm tham số maLop nếu người dùng có chọn lớp
        if ($scope.maLop && $scope.maLop !== "") {
            url += "&maLop=" + $scope.maLop;
        }

        console.log("Calling API: " + url);

        $http.get(url).then(function(res) {
            $scope.dsDiemDanh = res.data;
            
            if ($scope.dsDiemDanh.length == 0) {
                console.log("API trả về danh sách rỗng");
            }

            $scope.dsDiemDanh.forEach(function(item) {
                item.GioDenObject = parseTime(item.GioDen);
                item.GioVeObject = parseTime(item.GioVe);
                if (!item.TrangThai) item.TrangThai = "Có mặt";
            });

        }, function(err) {
            console.error("Lỗi API:", err);
            // In chi tiết lỗi ra để debug
            alert("Lỗi tải dữ liệu: " + (err.data && err.data.title ? err.data.title : "Lỗi kết nối")); 
        });
    };

    // --- CÁC HÀM LƯU / XÓA GIỮ NGUYÊN ---
    // ...

    // TỰ ĐỘNG CHẠY
    $scope.taiDuLieu();
});