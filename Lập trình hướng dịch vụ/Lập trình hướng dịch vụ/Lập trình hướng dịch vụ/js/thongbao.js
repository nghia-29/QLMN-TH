var app = angular.module('AppThongBao', []);

app.controller('ThongBaoController', function($scope, $http) {

    // =============================================================
    // 1. CẤU HÌNH ĐƯỜNG DẪN API
    // =============================================================
    // Lưu ý: Đổi cổng 6002 thành cổng API thực tế của bạn (nếu khác)
    var host = "http://localhost:6002"; 
    
    var apiThongBao = host + "/api/ThongBao"; 
    // API lấy danh sách học sinh (Mượn từ API Điểm danh hoặc API Học sinh)
    var apiHocSinh  = host + "/api/DiemDanh/get-all?maLop=MAM01"; 

    // Khởi tạo các biến dữ liệu
    $scope.dsThongBao = [];
    $scope.dsHocSinh = [];
    
    // Mặc định chế độ gửi
    $scope.cheDoGui = "ALL"; 

    // Đối tượng thông báo (Model)
    $scope.tb = {
        TieuDe: "",
        NoiDung: "",
        LoaiThongBao: "Chung", // Mặc định
        MaGiaoVien: "GV01",    // Có thể lấy từ localStorage khi đăng nhập
        MaHocSinh: null,       // Null nếu gửi cho cả lớp
        NguoiGui: "Giáo Viên CN",
        NguoiNhan: ""
    };

    // =============================================================
    // 2. HÀM TẢI DỮ LIỆU TỪ SERVER (READ)
    // =============================================================
    
    // Hàm 1: Lấy danh sách thông báo đã gửi
    $scope.loadData = function() {
        $http.get(apiThongBao + "/get-all").then(function(res) {
            if (res.data) {
                $scope.dsThongBao = res.data;
            }
        }, function(err) {
            console.error("Lỗi tải thông báo:", err);
        });
    };

    // Hàm 2: Lấy danh sách học sinh để chọn gửi riêng
    $scope.loadHocSinh = function() {
        $http.get(apiHocSinh).then(function(res) {
            // Xử lý logic: Vì API Điểm danh có thể trả về nhiều dòng của 1 học sinh (qua các ngày)
            // Nên ta cần lọc để lấy danh sách học sinh DUY NHẤT (Unique)
            var map = new Map();
            var danhSachDuyNhat = [];

            if (res.data && res.data.length > 0) {
                res.data.forEach(item => {
                    // Nếu chưa có mã học sinh này trong Map thì thêm vào list
                    if (!map.has(item.MaHocSinh)) {
                        map.set(item.MaHocSinh, true);
                        danhSachDuyNhat.push({
                            MaHocSinh: item.MaHocSinh,
                            HoTen: item.HoTen || item.TenHocSinh // Đề phòng API trả về tên biến khác nhau
                        });
                    }
                });
                $scope.dsHocSinh = danhSachDuyNhat;
            }
        }, function(err) {
            console.error("Lỗi tải học sinh:", err);
        });
    };

    // =============================================================
    // 3. HÀM GỬI THÔNG BÁO (CREATE)
    // =============================================================
    $scope.guiThongBao = function() {
        // Validate dữ liệu
        if (!$scope.tb.TieuDe || !$scope.tb.NoiDung) {
            alert("Vui lòng nhập đầy đủ Tiêu đề và Nội dung!");
            return;
        }

        // Xử lý người nhận
        if ($scope.cheDoGui == "ALL") {
            $scope.tb.NguoiNhan = "Toàn thể phụ huynh";
            $scope.tb.MaHocSinh = null; 
        } else {
            if (!$scope.tb.MaHocSinh) {
                alert("Vui lòng chọn học sinh cần gửi!");
                return;
            }
            // Tìm tên học sinh để lưu hiển thị cho đẹp
            var hs = $scope.dsHocSinh.find(x => x.MaHocSinh == $scope.tb.MaHocSinh);
            $scope.tb.NguoiNhan = "Phụ huynh em " + (hs ? hs.HoTen : "...");
        }

        // Gán thời gian gửi hiện tại (Nếu Backend không tự gán)
        $scope.tb.NgayGui = new Date();

        // GỌI API POST
        $http.post(apiThongBao + "/create", $scope.tb).then(function(res) {
            alert("✅ Gửi thông báo thành công!");
            
            // Tải lại danh sách để thấy tin mới
            $scope.loadData();
            
            // Xóa trắng form
            $scope.lamMoi();
        }, function(err) {
            alert("❌ Lỗi khi gửi: " + (err.data ? err.data : "Vui lòng kiểm tra Server"));
            console.log(err);
        });
    };

    // =============================================================
    // 4. HÀM XÓA THÔNG BÁO (DELETE)
    // =============================================================
    $scope.xoaThongBao = function(id) {
        if (!confirm("Bạn có chắc chắn muốn xóa tin nhắn này khỏi hệ thống?")) return;

        // GỌI API DELETE (Hoặc POST tùy cách backend bạn viết)
        // Cách 1: Nếu backend dùng phương thức DELETE: $http.delete(apiThongBao + "/delete/" + id)
        // Cách 2: Nếu backend dùng phương thức POST hoặc GET để xóa:
        $http.post(apiThongBao + "/delete?id=" + id).then(function(res) {
            alert("Đã xóa tin nhắn!");
            $scope.loadData(); // Tải lại bảng
        }, function(err) {
            alert("❌ Không thể xóa! Có thể do lỗi Server.");
        });
    };

    // =============================================================
    // 5. CÁC HÀM PHỤ TRỢ
    // =============================================================
    $scope.lamMoi = function() {
        $scope.tb.TieuDe = "";
        $scope.tb.NoiDung = "";
        $scope.tb.MaHocSinh = "";
        $scope.tb.LoaiThongBao = "Chung";
        $scope.cheDoGui = "ALL";
    };

    // --- KHỞI CHẠY LẦN ĐẦU KHI VÀO TRANG ---
    $scope.loadData();    // Gọi ngay hàm tải thông báo
    $scope.loadHocSinh(); // Gọi ngay hàm tải danh sách học sinh
});